(define Interval
  (lambda (start stop NrOfPoints)
    (/ (- stop start) (- NrOfPoints 1))))
; The procedure "GenerateSamplePositions" takes a start and stop value and a amount of sample points "sp" and
; returns a list of "sp" x-values equally spread out from start to stop.
; "start" is the start of the interval
; "stop" is the stop of the interval
; "CountOfPoints" is the number of x-values to generate in the interval
(define GenerateSamplePositions
  (letrec ((GeneratePositions
            (lambda (Interval Acc Stop)
            (if (> (+ (car Acc) Interval) Stop)
                Acc
                (GeneratePositions Interval (cons (+ (car Acc) Interval) Acc) Stop )))))
  (lambda (start stop CountOfPoints)
    (let ((interval (Interval start stop CountOfPoints)))
    (if (<= stop start)
         '()
         (reverse (GeneratePositions interval (list start) stop)))))))

;(GenerateSamplePositions 1 5 5) ; => 1 2 3 4 5
;(GenerateSamplePositions 0 5 6) ; => 0 1 2 3 4 5
;(GenerateSamplePositions 10 20 11) ; => 10 11 12 13 14 15 16 17 18 19 20
;(GenerateSamplePositions 0 1/2 6) ; => 0 0.1 0.2 0.3 0.4 0.5
(define GenerateSamplePositions2
  (letrec ((GeneratePositions
            (lambda (Interval Acc Start Value)
            (if (< Value Start)
                Acc
                (GeneratePositions Interval (cons Value Acc) Start (- Value Interval))))))
  (lambda (start Stop CountOfPoints)
    (let ((interval (Interval Start Stop CountOfPoints)))
    (if (<= Stop Start)
         '()
        (GeneratePositions interval '() Start Stop))))))
(GenerateSamplePositions2 1 5 5) ; => 1 2 3 4 5
;(GenerateSamplePositions2 0 5 6) ; => 0 1 2 3 4 5
;(GenerateSamplePositions2 10 20 11) ; => 10 11 12 13 14 15 16 17 18 19 20
;(GenerateSamplePositions2 0 1/2 6) ; => 0 0.1 0.2 0.3 0.4 0.5

;zip takes two lists and returns a single list, where each element is a pair of an element i from both lists.
(define zip
  (letrec
      ((innerzip
        (lambda (lst1 lst2 acc)
          (if (or (null? lst1) (null? lst2))
              acc
              (innerzip (cdr lst1) (cdr lst2) (cons (cons (car lst1) (car lst2)) acc))
          ))))
  (lambda (lst1 lst2)
    (reverse (innerzip lst1 lst2 '())))))

; The procedure "CreateSamplePairs" takes a function and a list of sample positions and returns a list of pairs with x and f(x)-values
; "func" is the function used to generate the y-values from the x-values
; "sp" is the sample positions which is the x-values to base the f(x)-values on.
(define CreateSamplePairs
  (letrec ((CreatePairs
            (lambda (func acc sp)
              (if (null? sp)
                  acc
                  (CreatePairs func (cons (func (car sp)) acc) (cdr sp))))))
  (lambda (func sp)
    (zip sp (reverse (CreatePairs func '() sp))))))
;(CreateSamplePairs (lambda (x) (* x x)) (list 1 2 3)) ; => ((1.1) (2.4) (3.9))

; The procedure "derivative" takes a function and returns the derived function.
; "func" is the function to create the derived function from
; It is pretty the same method found here: http://www.mathsisfun.com/calculus/derivatives-introduction.html
; Make the dx smaller to enhance the result
(define dx 0.001)

(define derivative
  (lambda (func)
    (lambda (x)
      (/ (- (func (+ x dx)) (func x))
         dx))))

; The procedure "CreateDerivativeGraphValues" takes a function and a list of sample points
; and returns a list of pairs with x and f'(x)-values
; "func" is the function to create the derivative function from
; "sp" is the sample positions to base the f'(x)-values on.
(define CreateDerivativeGraphValues
  (lambda (func sp)
      (CreateSamplePairs (derivative func) sp)))
(CreateDerivativeGraphValues (lambda (x) (* x x)) (list 1 2 3)) ; => ((1.2)(2.4)(3.6))

; The procedure "CreateIntegationGraphValues" takes a function, a start and stop value and a count of samples and
; returns the value of the integration of the function between start and stop with a number of samples
;http://www.mathcs.emory.edu/~cheung/Courses/170/Syllabus/07/rectangle-method.html
(define CreateIntegationGraphValues
  (letrec ((CreateIntegrationGraphValuesRec
            (lambda (sp interval acc)
              (if(null? (cdr sp))
                 acc
                 (CreateIntegrationGraphValuesRec (cdr sp) interval (+ acc (* interval (cdr (car sp))))))))) 
  (lambda (func start stop s)
    (CreateIntegrationGraphValuesRec (CreateSamplePairs func (GenerateSamplePositions start stop s)) (Interval start stop s)  0))))
;Test
(CreateIntegationGraphValues (lambda (x) (* x x)) 1 5 5000) ; => 41.3333

; By with stop and moving to start it is possible to spare a step and thereby making it better
(define CalculateIntegrationValue  
  (letrec ((CalculateIntegrationValueRecursive
            (lambda (func interval start value acc)
              (if (< value start)
                  acc
                  (CalculateIntegrationValueRecursive func interval start (- value interval) (+ acc (* interval (func value))))))))
  (lambda (func start stop CountOfSamples)
    (let ((interval (Interval start stop CountOfSamples)))
    (if (>= start stop)
        '()
        (CalculateIntegrationValueRecursive func interval start (- stop interval) 0))))))
(CalculateIntegrationValue (lambda (x) (* x x)) 1 5 10) ; => 41.3333
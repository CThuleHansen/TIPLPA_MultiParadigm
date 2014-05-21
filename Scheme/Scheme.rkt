(define Interval
  (lambda (start stop countOfPoints)
    (exact->inexact (/ (- stop start) (- countOfPoints 1)))))
; The procedure "GenerateSamplePositions" takes a start and stop value and a amount of sample points and
; returns a list of "sp" x-values equally spread out from start to stop.
; "start" is the start of the interval
; "stop" is the stop of the interval
; "CountOfPoints" is the number of x-values to generate in the interval
(define GenerateSamplePositions
  (letrec ((GeneratePositions
            (lambda (interval acc start value)
            (if (< value start)
                acc
                (GeneratePositions interval (cons value acc) start (- value interval))))))
  (lambda (start stop countOfPoints)
    (let ((interval (Interval start stop countOfPoints)))
    (if (<= stop start)
         '()
        (GeneratePositions interval '() start stop))))))
;(GenerateSamplePositions 1 5 5) ; => 1 2 3 4 5
;(GenerateSamplePositions 0 5 6) ; => 0 1 2 3 4 5
;(GenerateSamplePositions 10 20 11) ; => 10 11 12 13 14 15 16 17 18 19 20
;(GenerateSamplePositions 0 1/2 6) ; => 0 0.1 0.2 0.3 0.4 0.5

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
; "func" is the function used to generate the f(x)-values from the x-values
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

; The procedure "CreateFunctionSamplePairs" wraps the procedure "CreateSamplePairs". It takes a func, a start and stop value and a amount of sample points and
; returns a list of corresponding x and f(x) pairs.
; "func" is the function used to generate the f(x)-values from the x-values
; "start" is the start of the interval
; "stop" is the stop of the interval
; "CountOfPoints" is the number of x-values to generate in the interval
(define CreateFunctionSamplePairs
  (lambda (func start stop countOfPoints)
    (CreateSamplePairs func (GenerateSamplePositions start stop countOfPoints))))
;(CreateFunctionSamplePairs (lambda (x) (* x x)) 1 3 3); => ((1.1) (2.4) (3.9))

; The procedure "CreateDerivativeGraphValues" takes a function and a list of sample points
; and returns a list of pairs with x and f'(x)-values
; "func" is the function to create the derivative function from
; "sp" is the sample positions to base the f'(x)-values on.
; The inner procedure "derivative" takes a function and returns the derived function.
; "func" is the function to create the derived function from
; "dx" is a measure of deltax going to zero
; http://www.mathsisfun.com/calculus/derivatives-introduction.html
(define CreateDerivativeGraphValues
  (letrec ((derivative
            (lambda (func dx)
              (lambda (x)
              (exact->inexact (/ (- (func (+ x dx)) (func x)) dx))))))
  (lambda (func dx sp)
      (CreateSamplePairs (derivative func dx) sp))))
;(CreateDerivativeGraphValues (lambda (x) (* x x)) 0.001 (list 1 2 3)) ; => ((1.2)(2.4)(3.6))

(define CreateDerivativeFunctionSamplePairs
  (lambda (func dx start stop countOfPoints)
    (CreateDerivativeGraphValues func dx (GenerateSamplePositions start stop countOfPoints))))
;(CreateDerivativeFunctionSamplePairs (lambda (x) (* x x)) 0.001 1 3 3 ) ; => ((1.2)(2.4)(3.6))

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
;(CreateIntegationGraphValues (lambda (x) (* x x)) 1 5 5000) ; => 41.3333

; The procedure "CalculateIntegrationValue" takes a function, a start and stop value and a count of samples and
; returns the value of the integration of the function between start and stop with a number of samples
; "func" is the function to integrate
; "start" is the start of the interval
; "stop" is the stop of the interval
; "CountOfPoints" is the number of x-values to generate in the interval
; The inner procedure CalculateIntegrationValueRecursive takes a function, an interval, a start, a rectStart and an accumulator
; and returns the integration of the function. It uses the rectangle method and starts backwards. So the "start" parameter is actually where it should stop.
; "func" is the function to integrate
; "interval" is the width of the rectangles based on the rectangle method.
; "start" is the start of the x-values to integrate over
; "rectStart" is where to start the current rectangle
; "acc" is the accumulator.
; http://www.mathcs.emory.edu/~cheung/Courses/170/Syllabus/07/rectangle-method.html
(define CalculateIntegrationValue  
  (letrec ((CalculateIntegrationValueRecursive
            (lambda (func interval start rectStart acc)
              (if (< rectStart start)
                  acc
                  (CalculateIntegrationValueRecursive func interval start (- rectStart interval) (+ acc (* interval (func rectStart))))))))
  (lambda (func start stop countOfPoints)
    (let ((interval (Interval start stop countOfPoints)))
    (if (>= start stop)
        '()
        (CalculateIntegrationValueRecursive func interval start (- stop interval) 0))))))
;(CalculateIntegrationValue (lambda (x) (* x x)) 1 5 5000) ; => 41.3333
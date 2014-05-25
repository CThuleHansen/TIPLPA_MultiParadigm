; The procedure "CalcSamples" takes a start and stop value and a amount of sample points and
; returns a list of "sp" x-values equally spread out from start to stop. 
; Special cases:
;   <= 2 sample points in a interval where start does not equal stop returns '() - otherwise as stated in the description above.
;   >= 1 sample point in an interval where start equals stop returns a list with the start/stop point - otherwise '()
; For examples of usage see the tests below the procedure.
(define CalcSamples
  (letrec ((CalcSamplesRec
            (lambda (interval acc start value)
            (if (< value start)
                acc
                (CalcSamplesRec interval (cons value acc) start (- value interval))))))
    (let ((Interval
              (lambda (start stop samples)
                (if (> samples 1)
                    (exact->inexact (/ (- stop start) (- samples 1)))
                    (- stop start)))))
  (lambda (start stop samples)
    (let ((interval (Interval start stop samples)))
      (if (and (= start stop) (>= samples 1))
          (list start)
          (if (and (> stop start) (>= samples 2))
              (CalcSamplesRec interval '() start stop)
              '())))))))
; Tests
;(CalcSamples 1 5 -1); '()
;(CalcSamples 1 5 0); => '()
;(CalcSamples 1 5 1); => '() 
;(CalcSamples 1 1 1); => (1)
;(CalcSamples 1 1 50); => (1)
;(CalcSamples 1 1 0); => '()
;(CalcSamples 1 5 2); => (1 5)
;(CalcSamples 1 5 5) ; => (1 2 3 4 5)

; The procedure Zip takes two lists and returns a single list where each element is a pair of corresponding elements from the two lists.
; For examples of usage see the tests below the procedure.
(define Zip
  (letrec
      ((ZipRec
        (lambda (lst1 lst2 acc)
          (if (or (null? lst1) (null? lst2))
              acc
              (ZipRec (cdr lst1) (cdr lst2) (cons (cons (car lst1) (car lst2)) acc))
          ))))
  (lambda (lst1 lst2)
    (reverse (ZipRec lst1 lst2 '())))))
; Tests
;(Zip '(1 2 3) '(a b c)); => ((1.a)(2.b)(3.c))
;(Zip '(1 2) '(a b c)); => ((1.a)(2.b))
;(Zip '(1 2 3) '(a b)); => ((1.a)(2.b))

; The procedure "CalcFuncPairs" uses the procedure "CalcSamples" to generate the x-values
; and the uses the parameter func to generate the corresponding f(x)-values.
; For examples of usage see the tests below the procedure.
(define CalcFuncPairs
            (letrec ((CalcFuncVal
                      (lambda (func acc sp)
                        (if (null? sp)
                            acc
                            (CalcFuncVal func (cons (func (car sp)) acc) (cdr sp))))))
    (lambda (func start stop samples)
      (let ((sp (CalcSamples start stop samples)))
      (Zip sp (reverse (CalcFuncVal func '() sp)))))))
; Tests
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 -1); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 0); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 1); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 0); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 1); => ((1 . 1))
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 50); => ((1 . 1))
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 2); => ((1.1) (3.9))
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 3); => ((1 . 1) (2 . 4) (3 . 9))

; The procedure "CalcDeriFuncPairs" calculates the derived function of the parameter "func" and then passes
; it to the function CalcFuncPairs to generate pairs of x-values and corresponding f'(x) values.
; dx is a infinitesimal and should head towards 0. The smaller the value of dx is the better.
; For examples of usage see the tests below the procedure.
(define CalcDeriFuncPairs
  (letrec ((derivative
            (lambda (func dx)
              (lambda (x)
              (exact->inexact (/ (- (func (+ x dx)) (func x)) dx))))))
  (lambda (func dx start stop samples)
    (if (= dx 0)
        '()
        (CalcFuncPairs (derivative func dx) start stop samples)))))

; Tests. The procedure "CalcFuncPairs" has already been tested further up and therefore it is not necessary to test so many cases.
;(CalcDeriFuncPairs (lambda (x) (* x x)) 0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
;(CalcDeriFuncPairs (lambda (x) (* x x)) -0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
;(CalcDeriFuncPairs (lambda (x) (* x x)) 0 1 3 3) ; => '()

; The procedure "CalcInt" calcuates an approxmiation of the definite integralof a function over an interval.
; CalcFuncPairs is used to calculate the start of the rectangles and the height of the rectangles.
; For examples of usage see the tests below the procedure.
(define CalcInt
  (let ((CalcRectWidth
         (lambda (start stop countOfRect)
           (/ (- stop start) countOfRect))))
    (letrec ((calcIntVal
              (lambda (fsp width acc)
                (if (null? fsp)
                    acc
                    (calcIntVal (cdr fsp) width (+ acc (* width (cdr (car fsp)))))))))
    (lambda (func start stop rect)
      (if (and (>= stop start) (> rect 0))
          (calcIntVal (CalcFuncPairs func start (- stop (CalcRectWidth start stop rect)) rect) (CalcRectWidth start stop rect) 0)
          '())))))
; Tests   
;(CalcInt (lambda (x) (* x x)) 1 1 -1); => '()
;(CalcInt (lambda (x) (* x x)) 1 5 0); => '()
;(CalcInt (lambda (x) (* x x)) 1 1 1); => 0
;(CalcInt (lambda (x) (* x x)) 1 1 2); => 0
;(CalcInt (lambda (x) (* x x)) 1 5 1); => 4 because the function x*x has a height of 1^2=1 and a width of 5-1=4 with 1 rectangle.
;(CalcInt (lambda (x) (* x x)) 1 5 2); => 20 because the function x*x has 2 rectangles. 
; 1 rectangle at x=1 with height: 1*1=1 and width: 3-1=2 which gives the area: 1*2 = 2
; 1 rectangle at x=3 width height: 3*3=9 and width: 5-3=2 which gives the area: 9*2 = 18
; Total: 2+18 = 20.
;(CalcInt (lambda (x) (* x x)) 1 5 5000); => 124/3 ~= 41.3333... Increasing the amount of rectangles brings the result closer to 124/3
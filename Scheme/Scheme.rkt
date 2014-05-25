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

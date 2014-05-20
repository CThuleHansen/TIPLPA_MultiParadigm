;The procedure below creates sample points based on start x value, stop x value and number of sample points.
; The start and stop values are ALWAYS included to get a better looking graph
(define GenerateSamplePoints
  (letrec ((GeneratePoints
            (lambda (Interval Acc Stop)
            (if (> (+ (car Acc) Interval) Stop)
                (cons stop Acc)
                (GeneratePoints Interval (cons (+ (car Acc) Interval) Acc) Stop )))))
  (lambda (start stop dp)
    (let ((interval (/ (- stop start) dp)))
    (if (<= stop start)
         '()
         (cons start (reverse (GeneratePoints interval (list (+ start (- interval (/ interval 2)))) stop))))))))
;Test should output: 
(GenerateSamplePoints 0 10 11)

;zip takes two lists and returns a single list of the corresponding pairs of elements from the input lists.
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

;The procedure takes a function and a list of sample points and creates a list of pairs with x and y values
;sp is short for sample points, which means the x values
(define CreateGraphValues
  (letrec ((createPoints
            (lambda (func acc sp)
              (if (null? sp)
                  acc
                  (createPoints func (cons (func (car sp)) acc) (cdr sp))))))
  (lambda (func sp)
    (zip sp (reverse (createPoints func '() sp))))))
;Test should out: ((1.1) (2.4) (3.9))
;(CreateGraphValues (lambda (x) (* x x)) (list 1 2 3))

;The procedure returns the derivate function of the inputted function
; It is pretty the same method found here: http://www.mathsisfun.com/calculus/derivatives-introduction.html
; Make the dx smaller to enhance the result
(define dx 0.001)

(define derivative
  (lambda (f)
    (lambda (x)
      (/ (- (f (+ x dx)) (f x))
         dx))))

;The procedure takes a function and a list of sample points. 
;The function is then converted to the derivative and inputted to CreateGraphValues along with the sample points
(define CreateDerivativeGraphValues
  (lambda (func sp)
    (let ((funcderi (derivative func)))
      (CreateGraphValues (derivative func) sp))))
;Test should output ((1.2)(2.4)(3.6))
;(CreateDerivativeGraphValues (lambda (x) (* x x)) (list 1 2 3))



;Integration
;http://www.mathcs.emory.edu/~cheung/Courses/170/Syllabus/07/rectangle-method.html
;Divide the interval into n pieces of width (b-a)/n
(define IntWidth
  (lambda (a b n)
    (/(- b a) n)))

;The height of the area is the function value at the start of the interval
(define integration
  (letrec ((CreateInt
    (lambda (sp interval acc)
      (if (null? sp)
          acc
          (CreateInt (cdr sp) interval (+ (* interval (cdr (car sp))) acc))))))       
  (lambda (func start stop dp)
    (CreateInt (CreateGraphValues func (GenerateSamplePoints start stop dp)) (/ (- stop start) dp) 0))))
;Test
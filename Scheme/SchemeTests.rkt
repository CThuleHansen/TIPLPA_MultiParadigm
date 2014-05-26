(load "Scheme.rkt")

(equal? (CalcSamples 1 5 -1) '()); '()
(equal? (CalcSamples 1 5 0) '()); => '()
(equal? (CalcSamples 1 5 1) '()); => '()
(equal? (CalcSamples 1 1 1) '(1)); => (1)
(equal? (CalcSamples 1 1 50) '(1)); => (1)
(equal? (CalcSamples 1 1 0) '()); => '()
(equal? (CalcSamples 1 5 2) (list 1.0 5)); => (1.0 5)
(equal? (CalcSamples 5 1 2) '()); => '()
(equal? (CalcSamples 1 5 5) (list 1.0 2.0 3.0 4.0 5)); => (1.0 2.0 3.0 4.0 5)

(equal? (Zip '(1 2 3) '(a b c)) (list (cons 1 'a) (cons 2 'b) (cons 3 'c))); => ((1.a)(2.b)(3.c))
(equal? (Zip '(1 2) '(a b c)) (list (cons 1 'a) (cons 2 'b))); => ((1.a)(2.b))
(equal? (Zip '(1 2 3) '(a b)) (list (cons 1 'a) (cons 2 'b))); => ((1.a)(2.b))
(equal? (Zip '(1 2) '(a b c)) (list (cons 1 'a) (cons 2 'b))); => ((1.a)(2.b))

(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 3 -1) '()); => '()
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 3 0) '()); => '()
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 3 1) '()); => '()
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 1 0) '()); => '()
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 1 1) (list (cons 1 1))); => ((1 . 1))
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 1 50) (list (cons 1 1))); => ((1 . 1))
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 3 2) (list (cons 1.0 1.0) (cons 3 9))); => ((1.0 . 1.0) (3 . 9))
(equal? (CalcFuncPairs (lambda (x) (* x x)) 1 3 3) (list (cons 1.0 1.0) (cons 2.0 4.0) (cons 3 9))); => ((1.0 . 1.0) (2.0 . 4.0) (3 . 9))

;The procedure "CalcFuncPairs" has already been tested further up and therefore it is not necessary to test so many cases.
(CalcDeriFuncPairs (lambda (x) (* x x)) 0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
(CalcDeriFuncPairs (lambda (x) (* x x)) -0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
(equal? (CalcDeriFuncPairs (lambda (x) (* x x)) 0.00 1 3 3) '()) ; => '()
(equal? (CalcDeriFuncPairs (lambda (x) (* x x)) 0 1 3 3) '()) ; => '()

(equal? (CalcInt (lambda (x) (* x x)) 1 1 -1) '()); => '()
(equal? (CalcInt (lambda (x) (* x x)) 1 5 0) '()); => '()
(equal? (CalcInt (lambda (x) (* x x)) 1 1 1) 0); => 0
(equal? (CalcInt (lambda (x) (* x x)) 1 1 2) 0); => 0
(equal? (CalcInt (lambda (x) (* x x)) 1 5 1) 4); => 4 because the function x*x has a height of 1^2=1 and a width of 5-1=4 with 1 rectangle.
(equal? (CalcInt (lambda (x) (* x x)) 1 5 2) 20.0); => 20 because the function x*x has 2 rectangles. 
; 1 rectangle at x=1 with height: 1*1=1 and width: 3-1=2 which gives the area: 1*2 = 2
; 1 rectangle at x=3 width height: 3*3=9 and width: 5-3=2 which gives the area: 9*2 = 18
; Total: 2+18 = 20.
;(CalcInt (lambda (x) (* x x)) 1 5 5000); => 124/3 ~= 41.3333... Increasing the amount of rectangles brings the result closer to 124/3


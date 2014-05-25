(load "Scheme.rkt")

;(CalcSamples 1 5 -1); '()
;(CalcSamples 1 5 0); => '()
;(CalcSamples 1 5 1); => '()
;(CalcSamples 1 1 1); => (1)
;(CalcSamples 1 1 50); => (1)
;(CalcSamples 1 1 0); => '()
;(CalcSamples 1 5 2); => (1 5)
;(CalcSamples 5 1 2); => '()
;(CalcSamples 1 5 5); => (1 2 3 4 5)

;(Zip '(1 2 3) '(a b c)); => ((1.a)(2.b)(3.c))
;(Zip '(1 2) '(a b c)); => ((1.a)(2.b))
;(Zip '(1 2 3) '(a b)); => ((1.a)(2.b))

;(CalcFuncPairs (lambda (x) (* x x)) 1 3 -1); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 0); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 1); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 0); => '()
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 1); => ((1 . 1))
;(CalcFuncPairs (lambda (x) (* x x)) 1 1 50); => ((1 . 1))
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 2); => ((1.1) (3.9))
;(CalcFuncPairs (lambda (x) (* x x)) 1 3 3); => ((1 . 1) (2 . 4) (3 . 9))

;The procedure "CalcFuncPairs" has already been tested further up and therefore it is not necessary to test so many cases.
;(CalcDeriFuncPairs (lambda (x) (* x x)) 0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
;(CalcDeriFuncPairs (lambda (x) (* x x)) -0.001 1 3 3) ; => ((1 . 2)(2.4)(3.6))
;(CalcDeriFuncPairs (lambda (x) (* x x)) 0.00 1 3 3) ; => ((1 . 2)(2.4)(3.6))
;(CalcDeriFuncPairs (lambda (x) (* x x)) 0 1 3 3) ; => '()

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


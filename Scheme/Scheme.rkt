;The procedure below creates sample points based on start x value, stop x value and number of sample points.
(define GenerateSamplePoints
  (letrec ((GeneratePoints
            (lambda (Interval Acc Stop)
            (if (> (+ (car Acc) Interval) Stop)
                Acc
                (GeneratePoints Interval (cons (+ (car Acc) Interval) Acc) Stop )))))
  (lambda (start stop dp)
    (let ((interval (/ (- stop start) dp)))
    (if (<= stop start)
         '()
         (reverse (GeneratePoints interval (list (+ start (- interval (/ interval 2)))) stop)))))))

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
    
         
(define Interval
  (lambda (start stop countOfPoints)
    (/ (- stop start) (- countOfPoints 1))))

(define GenerateSamplePositions2
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
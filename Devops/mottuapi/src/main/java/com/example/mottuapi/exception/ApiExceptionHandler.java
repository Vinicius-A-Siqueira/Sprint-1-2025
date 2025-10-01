package com.example.mottuapi.exception;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import java.util.HashMap;
import java.util.Map;

@RestControllerAdvice
public class ApiExceptionHandler {

    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<?> handleResourceNotFound(ResourceNotFoundException ex) {
        Map<String, String> erros = new HashMap<>();
        erros.put("erro", ex.getMessage());
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(erros);
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<?> handleValidationException(MethodArgumentNotValidException ex) {
        Map<String, String> erros = new HashMap<>();
        ex.getBindingResult().getFieldErrors().forEach(e -> erros.put(e.getField(), e.getDefaultMessage()));
        return ResponseEntity.badRequest().body(erros);
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<?> handleGenericException(Exception ex) {
        Map<String, String> erros = new HashMap<>();
        erros.put("erro", "Erro interno");
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(erros);
    }
}

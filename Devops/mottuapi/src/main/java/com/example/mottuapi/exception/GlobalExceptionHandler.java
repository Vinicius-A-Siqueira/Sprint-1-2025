package com.example.mottuapi.exception;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import jakarta.validation.ConstraintViolationException;

@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(Exception.class)
    public ResponseEntity<?> handleException(Exception ex) {
        ex.printStackTrace();  // imprime stacktrace no console para debug

        // Retorna a mensagem da exceção no JSON para ajudar no debug
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(new ErrorResponse("Erro: " + ex.getMessage()));
    }

    static class ErrorResponse {
        private String erro;

        public ErrorResponse(String erro) {
            this.erro = erro;
        }

        public String getErro() {
            return erro;
        }

        public void setErro(String erro) {
            this.erro = erro;
        }
    }
}

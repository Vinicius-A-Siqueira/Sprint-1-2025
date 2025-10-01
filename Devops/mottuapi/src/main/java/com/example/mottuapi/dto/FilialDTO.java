package com.example.mottuapi.dto;

import jakarta.validation.constraints.*;
import lombok.*;

@Data
public class FilialDTO {
    @NotBlank
    private String nome;

    @NotBlank
    private String endereco;
}

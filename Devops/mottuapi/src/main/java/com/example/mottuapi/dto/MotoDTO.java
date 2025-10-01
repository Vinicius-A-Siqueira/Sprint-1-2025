package com.example.mottuapi.dto;

import jakarta.validation.constraints.*;
import lombok.*;

@Data
public class MotoDTO {
    @NotBlank
    private String placa;

    @NotBlank
    private String status;

    @NotNull
    private Long filialId;
}
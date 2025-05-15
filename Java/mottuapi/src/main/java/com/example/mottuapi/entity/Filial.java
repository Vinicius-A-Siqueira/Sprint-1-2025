package com.example.mottuapi.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.*;

import java.util.List;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "filial")
public class Filial {

    @Id
    @SequenceGenerator(name = "filial_seq", sequenceName = "FILIAL_SEQ", allocationSize = 1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "filial_seq")
    @Column(name = "id_filial")
    private Long id;

    @NotBlank
    @Column(name = "nome")
    private String nome;

    @NotBlank
    @Column(name = "endereco")
    private String endereco;

    @OneToMany(mappedBy = "filial")
    private List<Moto> motos;
}

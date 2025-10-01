package com.example.mottuapi.entity;

import lombok.*;
import jakarta.persistence.*;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.Collections;

import java.util.List;

@Entity
public class Patio {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String nome;

    private String endereco;

    @OneToMany(mappedBy = "patio", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Moto> motos;

    // Getters e Setters
    public Long getId() { return id; }

    public void setId(Long id) { this.id = id; }

    public String getNome() { return nome; }

    public void setNome(String nome) { this.nome = nome; }

    public String getEndereco() { return endereco; }

    public void setEndereco(String endereco) { this.endereco = endereco; }

    public List<Moto> getMotos() { return motos; }

    public void setMotos(List<Moto> motos) { this.motos = motos; }
}


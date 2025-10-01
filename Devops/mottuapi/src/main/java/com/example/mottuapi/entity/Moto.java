package com.example.mottuapi.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.*;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.Collections;
import java.util.List;

@Entity
public class Moto {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String placa;

    private String modelo;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "patio_id")
    private Patio patio;

    // Getters e Setters
    public Long getId() { return id; }

    public void setId(Long id) { this.id = id; }

    public String getPlaca() { return placa; }

    public void setPlaca(String placa) { this.placa = placa; }

    public String getModelo() { return modelo; }

    public void setModelo(String modelo) { this.modelo = modelo; }

    public Patio getPatio() { return patio; }

    public void setPatio(Patio patio) { this.patio = patio; }
}
package com.example.mottuapi.entity;

import jakarta.persistence.*;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.Collections;

@Entity
public class Usuario implements UserDetails {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String username;

    private String password;

    private String perfil; // exemplo: "ROLE_ADMIN" ou "ROLE_FUNCIONARIO"

    // Getters e Setters
    public Long getId() { return id; }

    public void setId(Long id) { this.id = id; }

    public String getUsername() { return username; }

    public void setUsername(String username) { this.username = username; }

    @Override
    public String getPassword() { return password; }

    public void setPassword(String password) { this.password = password; }

    public String getPerfil() { return perfil; }

    public void setPerfil(String perfil) { this.perfil = perfil; }

    // Métodos de UserDetails

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return Collections.singletonList(new SimpleGrantedAuthority(this.perfil));
    }

    @Override
    public boolean isAccountNonExpired() {
        return true; // conta não expirada
    }

    @Override
    public boolean isAccountNonLocked() {
        return true; // conta não bloqueada
    }

    @Override
    public boolean isCredentialsNonExpired() {
        return true; // credenciais não expiraram
    }

    @Override
    public boolean isEnabled() {
        return true; // conta está habilitada
    }
}
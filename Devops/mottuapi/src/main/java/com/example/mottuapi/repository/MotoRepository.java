package com.example.mottuapi.repository;

import com.example.mottuapi.entity.Moto;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface MotoRepository extends JpaRepository<Moto, Long> {
    // Métodos adicionais personalizados se necessário
}
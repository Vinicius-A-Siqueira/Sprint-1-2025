package com.example.mottuapi.repository;

import com.example.mottuapi.entity.Patio;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PatioRepository extends JpaRepository<Patio, Long> {
    // Métodos personalizados extras podem ser adicionados aqui
}

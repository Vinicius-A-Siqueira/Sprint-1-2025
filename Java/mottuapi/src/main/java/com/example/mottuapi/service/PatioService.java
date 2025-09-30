package com.example.mottuapi.service;

import com.example.mottuapi.entity.Patio;
import com.example.mottuapi.repository.PatioRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class PatioService {

    @Autowired
    private PatioRepository patioRepository;

    public List<Patio> listarTodos() {
        return patioRepository.findAll();
    }

    public Optional<Patio> buscar(Long id) {
        return patioRepository.findById(id);
    }

    public Patio salvar(Patio patio) {
        return patioRepository.save(patio);
    }

    public void excluir(Long id) {
        patioRepository.deleteById(id);
    }
}

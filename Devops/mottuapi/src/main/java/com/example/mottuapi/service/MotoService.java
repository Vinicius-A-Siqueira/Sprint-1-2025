package com.example.mottuapi.service;

import com.example.mottuapi.entity.Moto;
import com.example.mottuapi.repository.MotoRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class MotoService {

    @Autowired
    private MotoRepository motoRepository;

    public List<Moto> listarTodos() {
        return motoRepository.findAll();
    }

    public Optional<Moto> buscar(Long id) {
        return motoRepository.findById(id);
    }

    public Moto salvar(Moto moto) {
        return motoRepository.save(moto);
    }

    public void excluir(Long id) {
        motoRepository.deleteById(id);
    }
}
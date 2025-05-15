package com.example.mottuapi.service;

import com.example.mottuapi.entity.Filial;
import com.example.mottuapi.exception.ResourceNotFoundException;
import com.example.mottuapi.repository.FilialRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class FilialService {
    private final FilialRepository repository;

    public FilialService(FilialRepository repository) {
        this.repository = repository;
    }

    public List<Filial> listarTodas() {
        return repository.findAll();
    }

    public Filial buscarPorId(Long id) {
        return repository.findById(id).orElseThrow(() -> new ResourceNotFoundException("Filial não encontrada"));
    }

    public Filial salvar(Filial filial) {
        return repository.save(filial);
    }

    public Filial atualizar(Long id, Filial filial) {
        Filial existente = buscarPorId(id);
        existente.setNome(filial.getNome());
        existente.setEndereco(filial.getEndereco());
        return repository.save(existente);
    }

    public void deletar(Long id) {
        repository.delete(buscarPorId(id));
    }
}
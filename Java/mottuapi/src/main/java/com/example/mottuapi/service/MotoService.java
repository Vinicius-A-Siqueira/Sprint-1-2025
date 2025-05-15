package com.example.mottuapi.service;

import com.example.mottuapi.entity.Moto;
import com.example.mottuapi.entity.Filial;
import com.example.mottuapi.exception.ResourceNotFoundException;
import com.example.mottuapi.repository.MotoRepository;
import com.example.mottuapi.repository.FilialRepository;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

@Service
public class MotoService {
    private final MotoRepository motoRepository;
    private final FilialRepository filialRepository;

    public MotoService(MotoRepository motoRepository, FilialRepository filialRepository) {
        this.motoRepository = motoRepository;
        this.filialRepository = filialRepository;
    }

    @Cacheable("motos")
    public Page<Moto> listar(String status, Pageable pageable) {
        return motoRepository.findByStatusContainingIgnoreCase(status, pageable);
    }

    public Moto buscarPorId(Long id) {
        return motoRepository.findById(id).orElseThrow(() -> new ResourceNotFoundException("Moto não encontrada"));
    }

    public Moto salvar(Moto moto, Long filialId) {
        Filial filial = filialRepository.findById(filialId).orElseThrow(() -> new ResourceNotFoundException("Filial não encontrada"));
        moto.setFilial(filial);
        return motoRepository.save(moto);
    }

    public Moto atualizar(Long id, Moto motoAtualizado) {
        Moto moto = buscarPorId(id);
        moto.setPlaca(motoAtualizado.getPlaca());
        moto.setStatus(motoAtualizado.getStatus());
        return motoRepository.save(moto);
    }

    public void deletar(Long id) {
        motoRepository.delete(buscarPorId(id));
    }
}
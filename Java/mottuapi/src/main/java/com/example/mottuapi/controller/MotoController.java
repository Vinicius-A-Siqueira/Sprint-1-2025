package com.example.mottuapi.controller;

import com.example.mottuapi.dto.MotoDTO;
import com.example.mottuapi.entity.Moto;
import com.example.mottuapi.service.MotoService;
import jakarta.validation.Valid;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/motos")
public class MotoController {
    private final MotoService service;

    public MotoController(MotoService service) {
        this.service = service;
    }

    @GetMapping
    public Page<Moto> listar(@RequestParam(defaultValue = "") String status, Pageable pageable) {
        return service.listar(status, pageable);
    }

    @PostMapping
    public Moto criar(@RequestBody @Valid MotoDTO dto) {
        Moto moto = new Moto(null, dto.getPlaca(), dto.getStatus(), null);
        return service.salvar(moto, dto.getFilialId());
    }

    @PutMapping("/{id}")
    public Moto atualizar(@PathVariable Long id, @RequestBody @Valid MotoDTO dto) {
        Moto moto = new Moto(id, dto.getPlaca(), dto.getStatus(), null);
        return service.atualizar(id, moto);
    }

    @DeleteMapping("/{id}")
    public void deletar(@PathVariable Long id) {
        service.deletar(id);
    }
}
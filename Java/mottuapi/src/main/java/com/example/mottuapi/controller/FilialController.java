package com.example.mottuapi.controller;

import com.example.mottuapi.dto.FilialDTO;
import com.example.mottuapi.entity.Filial;
import com.example.mottuapi.service.FilialService;
import jakarta.validation.Valid;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/filiais")
public class FilialController {
    private final FilialService service;

    public FilialController(FilialService service) {
        this.service = service;
    }

    @GetMapping
    public List<Filial> listar() {
        return service.listarTodas();
    }

    @PostMapping
    public Filial criar(@RequestBody @Valid FilialDTO dto) {
        Filial filial = new Filial(null, dto.getNome(), dto.getEndereco(), null);
        return service.salvar(filial);
    }

    @PutMapping("/{id}")
    public Filial atualizar(@PathVariable("id") Long id, @RequestBody @Valid FilialDTO dto) {
        Filial filial = new Filial(id, dto.getNome(), dto.getEndereco(), null);
        return service.atualizar(id, filial);
    }

    @DeleteMapping("/{id}")
    public void deletar(@PathVariable("id") Long id) {
        service.deletar(id);
    }
}

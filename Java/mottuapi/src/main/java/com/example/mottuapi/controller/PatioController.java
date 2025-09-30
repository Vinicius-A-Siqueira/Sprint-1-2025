package com.example.mottuapi.controller;

import com.example.mottuapi.entity.Patio;
import com.example.mottuapi.service.PatioService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/patio")
public class PatioController {

    @Autowired
    private PatioService patioService;

    // Listar todos os patios
    @GetMapping
    public String listar(Model model) {
        List<Patio> patios = patioService.listarTodos();
        model.addAttribute("patios", patios);
        return "patio/list";
    }

    // Página para criar novo patio
    @GetMapping("/new")
    public String novo(Model model) {
        model.addAttribute("patio", new Patio());
        return "patio/form";
    }

    // Salvar patio (novo ou edição)
    @PostMapping("/save")
    public String salvar(@Valid Patio patio, BindingResult result, Model model) {
        if (result.hasErrors()) {
            return "patio/form";
        }
        patioService.salvar(patio);
        return "redirect:/patio";
    }

    // Página para editar patio
    @GetMapping("/edit/{id}")
    public String editar(@PathVariable("id") Long id, Model model) {
        Patio patio = patioService.buscar(id).orElse(null);
        model.addAttribute("patio", patio);
        return "patio/form";
    }

    // Deletar patio
    @GetMapping("/delete/{id}")
    public String deletar(@PathVariable("id") Long id) {
        patioService.excluir(id);
        return "redirect:/patio";
    }
}

package com.example.mottuapi.controller;

import com.example.mottuapi.entity.Moto;
import com.example.mottuapi.entity.Patio;
import com.example.mottuapi.service.MotoService;
import com.example.mottuapi.service.PatioService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/moto")
public class MotoController {

    @Autowired
    private MotoService motoService;

    @Autowired
    private PatioService patioService;

    @GetMapping
    public String listar(Model model) {
        List<Moto> motos = motoService.listarTodos();
        model.addAttribute("motos", motos);
        return "moto/list";
    }

    @GetMapping("/new")
    public String novo(Model model) {
        model.addAttribute("moto", new Moto());
        model.addAttribute("patios", patioService.listarTodos());
        return "moto/form";
    }

    @PostMapping("/save")
    public String salvar(@Valid Moto moto, BindingResult result, Model model) {
        if (result.hasErrors()) {
            model.addAttribute("patios", patioService.listarTodos());
            return "moto/form";
        }
        motoService.salvar(moto);
        return "redirect:/moto";
    }

    @GetMapping("/edit/{id}")
    public String editar(@PathVariable Long id, Model model) {
        Moto moto = motoService.buscar(id).orElse(null);
        model.addAttribute("moto", moto);
        model.addAttribute("patios", patioService.listarTodos());
        return "moto/form";
    }

    @GetMapping("/delete/{id}")
    public String deletar(@PathVariable Long id) {
        motoService.excluir(id);
        return "redirect:/moto";
    }
}

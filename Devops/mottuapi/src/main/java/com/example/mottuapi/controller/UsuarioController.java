package com.example.mottuapi.controller;

import com.example.mottuapi.entity.Usuario;
import com.example.mottuapi.service.UsuarioService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;

@Controller
public class UsuarioController {

    @Autowired
    private UsuarioService usuarioService;

    @GetMapping("/usuario/novo")
    public String novoUsuarioForm(Model model) {
        model.addAttribute("usuario", new Usuario());
        return "form";
    }

    @PostMapping("/usuario/save")
    public String salvarUsuario(@ModelAttribute Usuario usuario) {
        usuarioService.salvar(usuario);
        return "redirect:/login?registroSucesso";
    }
}

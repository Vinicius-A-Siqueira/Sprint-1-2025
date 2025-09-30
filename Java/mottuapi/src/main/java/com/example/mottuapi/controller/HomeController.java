package com.example.mottuapi.controller;

import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class HomeController {

    @GetMapping("/login")
    public String login() {
        return "login"; // Thymeleaf page
    }

    @GetMapping("/dashboard")
    public String dashboard(Authentication authentication, Model model) {
        // mostrar dashboard customizado por papel
        String role = authentication.getAuthorities().stream()
                .map(auth -> auth.getAuthority())
                .findFirst().orElse("ROLE_FUNCIONARIO");
        model.addAttribute("role", role);
        return "dashboard";
    }
}

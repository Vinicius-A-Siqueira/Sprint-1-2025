package com.example.mottuapi;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cache.annotation.EnableCaching;

@SpringBootApplication
@EnableCaching

public class MottuApiApplication {

    public static void main(String[] args) {
        SpringApplication.run(MottuApiApplication.class, args);
    }
}

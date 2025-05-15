package org.example.onderhoudsbuddyjava;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
            .csrf(csrf -> csrf.disable())  // CSRF uitschakelen voor API's
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/users/**").permitAll()  // Sta alle gebruiker-gerelateerde endpoints toe
                .anyRequest().authenticated()
            );

        return http.build();
    }
}

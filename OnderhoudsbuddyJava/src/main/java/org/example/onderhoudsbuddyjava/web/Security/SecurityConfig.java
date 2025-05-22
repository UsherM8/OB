package org.example.onderhoudsbuddyjava.web.Security;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Lazy;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.CorsConfigurationSource;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;

import java.util.List;

@Configuration
public class SecurityConfig {

    private final JwtUtil jwtUtil;
    private final UserDetailsService userDetailsService;

    public SecurityConfig(JwtUtil jwtUtil, @Lazy UserDetailsService userDetailsService) {
        this.jwtUtil = jwtUtil;
        this.userDetailsService = userDetailsService;
    }

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
                .csrf(csrf -> csrf.disable())
                .cors(cors -> cors.configurationSource(corsConfigurationSource())) // Cors configuratie toevoegen
                .authorizeHttpRequests(authorize -> authorize
                    .anyRequest().permitAll() // Sta alle verzoeken toe
            );

        return http.build();
    }

    @Bean
    public BCryptPasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }

    @Bean
    public AuthenticationManager authenticationManager(AuthenticationConfiguration config) throws Exception {
        return config.getAuthenticationManager();
    }

    @Bean
    public JwtAuthFilter jwtAuthFilter() {
        return new JwtAuthFilter(jwtUtil, userDetailsService);
    }

    @Bean
    public CorsConfigurationSource corsConfigurationSource() {
        CorsConfiguration configuration = new CorsConfiguration();
        configuration.setAllowedOrigins(List.of("http://localhost:5173")); // Voeg toegestane origins toe
        configuration.setAllowedMethods(List.of("GET", "POST", "PUT", "DELETE", "OPTIONS")); // Toegestane methodes
        configuration.setAllowedHeaders(List.of("*")); // Sta alle headers toe
        configuration.setAllowCredentials(true); // Sta cookies en credentials toe

        UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
        source.registerCorsConfiguration("/**", configuration); // Pas configuratie toe op alle URL's
        return source;
    }
}

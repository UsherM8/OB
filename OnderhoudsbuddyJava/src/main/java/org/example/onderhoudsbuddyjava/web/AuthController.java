package org.example.onderhoudsbuddyjava.web;

import org.example.onderhoudsbuddyjava.business.UserService;
import org.example.onderhoudsbuddyjava.web.Security.JwtUtil;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.userdetails.User;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api/auth")
public class AuthController {

    private final AuthenticationManager authenticationManager;
    private final JwtUtil jwtUtil;
    private final UserService userService;  // Voeg UserService hier toe

    public AuthController(AuthenticationManager authenticationManager, JwtUtil jwtUtil, UserService userService) {
        this.authenticationManager = authenticationManager;
        this.jwtUtil = jwtUtil;
        this.userService = userService;  // Initialiseer de UserService
    }

    @PostMapping("/login")
    public Map<String, String> login(@RequestBody Map<String, String> request) {
        String email = request.get("email");
        String password = request.get("password");

        try {
            // Authenticeren
            Authentication authentication = authenticationManager.authenticate(
                    new UsernamePasswordAuthenticationToken(email, password)
            );

            User user = (User) authentication.getPrincipal();
            String token = jwtUtil.generateToken(user.getUsername());

            // Haal aanvullende gegevens uit de database via een UserService
            Integer userId = userService.findUserByEmail(email).getUserId();

            // Bouw de response
            Map<String, String> response = new HashMap<>();
            response.put("token", token);
            response.put("userId", userId.toString());
            return response;

        } catch (BadCredentialsException e) {
            throw new RuntimeException("Ongeldige inloggegevens.");
        }
    }

    @PostMapping("/logout")
    public Map<String, String> logout() {
        Map<String, String> response = new HashMap<>();
        response.put("message", "Uitgelogd");
        return response;
    }
}

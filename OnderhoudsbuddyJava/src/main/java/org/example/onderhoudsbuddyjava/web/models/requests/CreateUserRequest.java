package org.example.onderhoudsbuddyjava.web.models.requests;

import lombok.Data;

@Data
public class CreateUserRequest {
    private String firstName;
    private String lastName;
    private int birthDate;
    private String email;
    private String password;
    private String type;
}

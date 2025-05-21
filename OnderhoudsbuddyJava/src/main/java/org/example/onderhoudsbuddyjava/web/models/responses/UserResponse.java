package org.example.onderhoudsbuddyjava.web.models.responses;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Date;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class UserResponse {
    private Integer userId;
    private String firstName;
    private String lastName;
    private String password;
    private Date birthDate;
    private String email;
    private String type;
}

package org.example.onderhoudsbuddyjava.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.Date;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class

UserDto {
    private Integer userId;
    private String firstName;
    private String lastName;
    private String password;
    private Date birthDate;
    private String email;
    private String type;
}

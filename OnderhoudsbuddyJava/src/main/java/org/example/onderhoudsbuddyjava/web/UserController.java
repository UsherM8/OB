package org.example.onderhoudsbuddyjava.web;

import org.example.onderhoudsbuddyjava.domain.dto.UserDto;
import org.example.onderhoudsbuddyjava.domain.serviceinterfaces.IUserService;
import org.example.onderhoudsbuddyjava.web.models.requests.CreateUserRequest;
import org.example.onderhoudsbuddyjava.web.models.responses.UserResponse;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/users")
public class UserController {

    private final IUserService userService;

    public UserController(IUserService userService) {
        this.userService = userService;
    }

    @GetMapping
    public ResponseEntity<List<UserResponse>> getAllUsers() {
        List<UserResponse> users = userService.getAllUsers().stream()
                .map(this::mapToResponse)
                .collect(Collectors.toList());
        return ResponseEntity.ok(users);
    }

    @GetMapping("/{id}")
    public ResponseEntity<UserResponse> getUserById(@PathVariable Integer id) {
        UserDto user = userService.getUserById(id);
        return ResponseEntity.ok(mapToResponse(user));
    }

    @PostMapping
    public ResponseEntity<UserResponse> createUser(@RequestBody CreateUserRequest request) {
        UserDto dto = new UserDto(null, request.getFirstName(), request.getLastName(), request.getPassword(), request.getBirthDate(), request.getEmail(), request.getType());
        UserDto created = userService.createUser(dto,request.getPassword());
        return new ResponseEntity<>(mapToResponse(created), HttpStatus.CREATED);
    }

    @PutMapping("/{id}")
    public ResponseEntity<UserResponse> updateUser(
        @PathVariable Integer id,
        @RequestBody CreateUserRequest request
    ) {
        UserDto dto = new UserDto(id, request.getFirstName(), request.getLastName(), request.getPassword(), request.getBirthDate(), request.getEmail(), request.getType());
        UserDto updateUser = userService.updateUser(dto);
        return new ResponseEntity<>(mapToResponse(updateUser), HttpStatus.OK);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteUser(@PathVariable Integer id)
    {
        userService.deleteUser(id);
        return ResponseEntity.noContent().build();
    }



private UserResponse mapToResponse(UserDto dto) {
    if (dto == null) {
        throw new IllegalArgumentException("Gebruikersgegevens zijn niet beschikbaar!");
    }
    return new UserResponse(
        dto.getUserId(),
        dto.getFirstName(),
        dto.getLastName(),
        dto.getPassword(),
        dto.getBirthDate(),
        dto.getEmail(),
        dto.getType()
    );
}
}

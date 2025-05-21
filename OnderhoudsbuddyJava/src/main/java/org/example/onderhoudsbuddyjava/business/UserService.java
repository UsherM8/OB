package org.example.onderhoudsbuddyjava.business;

import org.example.onderhoudsbuddyjava.dal.entities.UserEntity;
import org.example.onderhoudsbuddyjava.dal.repositories.UserRepository;
import org.example.onderhoudsbuddyjava.domain.dto.UserDto;
import org.example.onderhoudsbuddyjava.domain.serviceinterfaces.IUserService;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class UserService implements IUserService, UserDetailsService {

    private final UserRepository userRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;

    public UserService(UserRepository userRepository, BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.userRepository = userRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    @Override
    public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
        // Zoek de gebruiker op via het e-mailadres
        UserEntity user = userRepository.findByEmail(email)
                .orElseThrow(() -> new UsernameNotFoundException("Gebruiker niet gevonden met email: " + email));

        // Maak een UserDetails-object voor Spring Security
        return User.builder()
                .username(user.getEmail())
                .password(user.getPassword()) // Gehashte wachtwoord
                .roles(user.getType()) // Zorg dat Role-based access werkt
                .build();
    }

    // Bestaande methoden
    public List<UserDto> getAllUsers() {
        return userRepository.findAll().stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public UserDto getUserById(Integer id) {
        return userRepository.findById(id)
                .map(this::mapToDto)
                .orElseThrow(() -> new RuntimeException("User not found"));
    }

    public UserDto createUser(UserDto dto, String password) {
        UserEntity entity = mapToEntity(dto);
        String encodedPassword = bCryptPasswordEncoder.encode(password);
        entity.setPassword(encodedPassword);
        UserEntity saved = userRepository.save(entity);
        return mapToDto(saved);
    }

    public UserDto updateUser(UserDto dto) {
        UserEntity entity = userRepository.findById(dto.getUserId())
                .orElseThrow(() -> new RuntimeException("User not found"));

        entity.setFirstName(dto.getFirstName());
        entity.setLastName(dto.getLastName());
        entity.setPassword(bCryptPasswordEncoder.encode(dto.getPassword())); // Versleutel wachtwoord
        entity.setBirthDate(dto.getBirthDate());
        entity.setEmail(dto.getEmail());
        entity.setType(dto.getType());

        return mapToDto(userRepository.save(entity));
    }

    public Boolean deleteUser(Integer id) {
        if (!userRepository.existsById(id)) {
            return false;
        }
        userRepository.deleteById(id);
        return true;
    }

    private UserDto mapToDto(UserEntity entity) {
        return new UserDto(
                entity.getUserId(),
                entity.getFirstName(),
                entity.getLastName(),
                entity.getPassword(),
                entity.getBirthDate(),
                entity.getEmail(),
                entity.getType()
        );
    }
    private UserEntity mapToEntity(UserDto dto) {
        return UserEntity.builder()
                .userId(dto.getUserId()) // optional if generating in DB
                .firstName(dto.getFirstName())
                .lastName(dto.getLastName())
                .birthDate(dto.getBirthDate())
                .email(dto.getEmail())
                .type(dto.getType())
                .build();
    }
}

package org.example.onderhoudsbuddyjava.domain.serviceinterfaces;

import org.example.onderhoudsbuddyjava.domain.dto.UserDto;
import java.util.List;

public interface IUserService {
    List<UserDto> getAllUsers();
    UserDto getUserById(Integer id);
    UserDto createUser(UserDto userDto, String password);
    UserDto updateUser(UserDto userDto);
    Boolean deleteUser(Integer id);
}

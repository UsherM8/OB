package org.example.onderhoudsbuddyjava.dal.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.example.onderhoudsbuddyjava.dal.entities.UserEntity;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.UUID;

@Repository
public interface UserRepository extends JpaRepository<UserEntity, Integer> {
    Optional<UserEntity> findByEmail(String email);
}

describe('Gebruiker logt in', () => {
    it('logt succesvol in met geldige gegevens', () => {
        cy.visit('http://localhost:5173/login');

        // Vul email en wachtwoord in
        cy.get('input[name="email"]').type('jan@example.com');
        cy.get('input[name="password"]').type('Wachtwoord123!');

        // Klik op de inlogknop
        cy.get('button[type="submit"]').click();

        // Controleer of we op /home komen
        cy.url().should('include', '/home');

        // Extra check of gebruiker zichtbaar is (optioneel)
        cy.contains('Welkom'); // afhankelijk van jouw implementatie
    });
});

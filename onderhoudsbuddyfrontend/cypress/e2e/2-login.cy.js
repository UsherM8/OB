describe('Gebruiker logt in', () => {
    it('logt succesvol in met geldige gegevens', () => {
        cy.visit('http://localhost:5173/login');

        cy.get('input[name="email"]').type('jan@example.com');
        cy.get('input[name="password"]').type('Wachtwoord123!');

        cy.get('button[type="submit"]').click();

        cy.url().should('include', '/home');

        cy.contains('Welkom');
    });
});

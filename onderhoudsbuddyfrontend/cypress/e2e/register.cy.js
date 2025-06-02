describe('Account aanmaken', () => {
    it('maakt een nieuw account aan', () => {
        cy.visit('http://localhost:5173/register');

        cy.get('input[name="firstName"]').type('Jan');
        cy.get('input[name="lastName"]').type('Jansen');
        cy.get('input[name="birthDate"]').type('2000-01-01');
        cy.get('input[name="email"]').type('jan@example.com');
        cy.get('input[name="password"]').type('Wachtwoord123!');
        cy.get('button[type="submit"]').click();

        cy.url().should('include', '/login');
    });
});

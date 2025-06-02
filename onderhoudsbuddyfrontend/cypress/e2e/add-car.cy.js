describe('Auto toevoegen', () => {
    beforeEach(() => {
        window.localStorage.setItem('token', 'jouw-valid-jwt-token');
        window.localStorage.setItem('userInfo', JSON.stringify({ id: 1 }));

        cy.visit('http://localhost:5173/SearchCar');
    });

    it('voegt een auto toe met een kenteken', () => {
        cy.get('input[name="licensePlate"]').type('12ABC3');

        cy.get('button[type="submit"], button').contains('Toevoegen').click();

        cy.url().should('include', '/UserCar');

        cy.contains('12ABC3');
    });
});

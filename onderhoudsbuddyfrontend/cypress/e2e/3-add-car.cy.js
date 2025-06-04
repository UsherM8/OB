describe('Auto toevoegen', () => {
    beforeEach(() => {
        window.localStorage.setItem('token', 'jouw-valid-jwt-token');
        window.localStorage.setItem('userInfo', JSON.stringify({ id: 1 }));

        cy.visit('http://localhost:5173/CarRegister');
    });

    it('voegt een auto toe met een kenteken', () => {
        cy.get('input[name="licensePlate"]').type('83ZSJT');
        cy.get('input[name="mileage"]').type('100');

        cy.get('button[type="submit"], button').contains('Auto toevoegen').click();

        cy.url().should('include', '/UserCar');

        cy.contains('83ZSJT');
    });
});

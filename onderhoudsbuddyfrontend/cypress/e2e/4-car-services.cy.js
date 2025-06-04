describe('Services ophalen voor auto', () => {
    beforeEach(() => {
        localStorage.setItem('token', 'jouw-valid-token');
        localStorage.setItem('userInfo', JSON.stringify({ id: 1 }));
    });

    it('laat services zien voor auto ID 1', () => {
        cy.visit('http://localhost:5173/CarService/1');

        cy.contains('Services voor auto 1');
        cy.get('.service-card').should('exist');
    });
});

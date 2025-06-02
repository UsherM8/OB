describe("Ophalen auto's van gebruiker", () => {
    it("toont auto's in UserCar", () => {
        // Zorg dat er een geldige token in localStorage zit
        localStorage.setItem('token', 'JOUW_TEST_JWT_TOKEN'); // <- vervang indien nodig

        // En een geldige userInfo zodat de frontend weet wie ingelogd is
        localStorage.setItem('userInfo', JSON.stringify({ id: 1 }));

        cy.visit('http://localhost:5173/UserCar');

        cy.contains('Mijn Auto\'s');
        cy.get('.car-card').should('have.length.at.least', 1); // verwacht minimaal 1 auto
    });
});

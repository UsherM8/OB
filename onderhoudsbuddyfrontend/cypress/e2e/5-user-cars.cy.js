describe("Ophalen auto's van gebruiker", () => {

    after(() => {
        cy.request('POST', 'http://localhost:8080/api/test/cleanupjava');
        cy.request('POST', 'https://localhost:7259/api/test/cleanupdotnet');
    });

    it("toont auto's in UserCar", () => {
        localStorage.setItem('userInfo', JSON.stringify({ id: 1 }));

        cy.visit('http://localhost:5173/UserCar');

        cy.contains('Mijn Auto\'s');
        cy.get('.car-card').should('have.length.at.least', 1);
    });
});

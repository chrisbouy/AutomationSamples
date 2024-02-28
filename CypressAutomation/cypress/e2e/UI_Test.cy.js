const USERNAME = Cypress.env('USERNAME')
const PASSWORD = Cypress.env('PASSWORD')
const ITEM_ID = 2;

describe('UI tests for DemoBlaze website', () => {

    beforeEach(() => {
        cy.visit('/')
    })

    it('Confirm There Are Only 3 Categories', () => {
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group').should('be.visible')
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group > #cat').contains('CATEGORIES')
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group > #itemc').should('have.length', 3)
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group > #itemc').should('contain', 'Phones')
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group > #itemc').should('contain', 'Laptops')
        cy.get('#contcont > :nth-child(1) > .col-lg-3 > .list-group > #itemc').should('contain', 'Monitors')
    })

    it('Each Item in "Home" Page Shows The Price Tag', () => {
        cy.get('.active > .nav-link').should('be.visible').contains('Home').click()
        cy.url().should('contain', '/index.html')
        cy.get('#tbodyid').should('be.visible')
        // we are iterating through items in table of products to make sure they all have price tags
        cy.get('#tbodyid').each(($div) => {
            cy.get('.card > .card-block > h5').should('exist')
        })
    })

    it('Video is Not Playing Automatically', () => {
        //video isn't visible until about is clicked
        cy.get('#videoModal').should('have.css', 'display', 'none')
        cy.get(':nth-child(3) > .nav-link').should('be.visible').contains('About us').click()
        cy.get('#videoModal').should('have.css', 'display', 'block')
        cy.get('#example-video').should('have.class', 'vjs-paused')
        cy.get('#example-video').should('not.have.class', 'vjs-playing')
        cy.get('#example-video').should('not.have.class', 'vjs-has-started')
    })

    it('Adding 1 Item to Cart of Logged-In User', () => {
        cy.Login(USERNAME, PASSWORD)
        //  ITEM_ID variable that can be manually adjusted
        const itemLocation = '#tbodyid > div:nth-child(' + ITEM_ID + ')'
        // check that there are 9 items on the home page
        cy.get('#tbodyid').find('.col-lg-4').its('length').should('eq', 9)
        // assert item details and add
        cy.get(itemLocation).find('.card-title').then($name => {
            const itemName = $name.text()
            cy.get(itemLocation).find('.card-img-top').click()
            cy.url().should('contain', 'prod.html?idp_=' + ITEM_ID)
            cy.get('.name').contains(itemName)
            cy.get('.col-sm-12 > .btn').click()
            // check correct alert message
            cy.on('window:alert', (AlertText) => {
                expect(AlertText).eql('Product added.')
            })
            //additional check to make sure correct item is in cart
            cy.get('#cartur').click()
            cy.get('#tbodyid > .success').should('length', 1).contains(itemName)
            // clean cart
            cy.get('#tbodyid > tr > td:nth-child(4) > a').click()
        })
    })
})

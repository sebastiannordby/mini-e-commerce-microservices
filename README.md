# MiniECommerce
## Arbeidskrav - PG3402 – Microservices – Sebastian Nordby

[![.NET](https://github.com/sebastiannordby/mini-e-commerce-microservices/actions/workflows/dotnet.yml/badge.svg)](https://github.com/sebastiannordby/mini-e-commerce-microservices/actions/workflows/dotnet.yml)

### Overview
The aim for this project is to create an e-commerce website. To support this, the project utilizes a microservice approach for the architecture. By separating the different services, we can achieve a high performant and flexible system. 

The system is made of 4 different main services: product, basket, order and purchase. Different use cases for the services are implied by their naming. 

From the company’s perspective the system will allow the administration to view and view orders, view and manage baskets and maintain products. 

The user will interact with a simple frontend that allows the user to browse products, and them to cart, place an order and checkout/purchase. There will also be functionality for viewing order history. 
### Prioritized user stories
#### *Browse product catalog*
As a user I want to browse the different products the company has to offer. To do this efficiently I would like to have different filters and search functionality to make targeting certain products and categories easily. As a user I often don’t have much time and need the search and filtering to be efficient, or else I would leave the shop to find another vendor.
#### *Place an order*
As a user I want to be able to add products to my shopping cart as I go through the catalog. But I don’t want to place the order immediately. Sometimes I just want to accumulate the whole list and see the total. Therefore, I suggest having a shopping cart where I can see the total and if the products I have added are in stock. When placing an order, I don’t want to manually enter my address every time. A better solution will be to have a setting for my profile where I can setup a delivery- and invoice-address. 
#### *Suggestions*
As a user I would like to have suggestions based on my previous orders. Let’s say the company is Walmart, but I only purchase products in a certain category. Ex. Technology. I don’t want to scroll through all the trousers and socks just to get to the technology section. The suggestions should be the first thing I see when I enter the website, given that I have made a purchase here before.
### Requirements
There are a few requirements that flow from the user stories specified above. The user related problems are the most important, but to achieve this we need data, and therefore I think the best place to start will be on the administrative. 

The administrative functions should be, in order of importance: maintaining products, managing orders and maintaining baskets (for customer support).

The user functions should be, in order of importance: log in, browse products, add them to basket, place an order, view order history and profile settings.
### Architecture
Since this is a microservices driven kind of architecture, the different services should communicate with a gateway. This allows us to do client-side load balancing and can help us finding dependencies between the services. The main services in this project are user-service, basket-service, order-service, product-service and purchase-service. The frontend will also communicate with the gateway. 

![Et bilde som inneholder skjermbilde, tekst, design

[Automatisk generert beskrivelse](architecture.png)
](https://github.com/sebastiannordby/mini-e-commerce-microservices/blob/main/architecture.png)
#### *Final notes*
The course is taken in Java, but to challenge myself I have chose to go with Dotnet C#. This to get a deeper understanding of different technologies and forces me to get my hands dirty. 

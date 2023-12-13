# MiniECommerce

## Arbeidskrav - PG3402 – Microservices – Sebastian Nordby

[![.NET](https://github.com/sebastiannordby/mini-e-commerce-microservices/actions/workflows/dotnet.yml/badge.svg)](https://github.com/sebastiannordby/mini-e-commerce-microservices/actions/workflows/dotnet.yml)

### Building and Running the Project
In the root directory run the following commands, in the written order: 

- Command to build: docker-compose build
- Command to run the build: docker-compose up

When the project is up and running, you can navigate to the web application at: http://localhost:50010/ 

### Architecture 

![image](/images/overview.png)

### User manual

<details>
    <summary>Profile</summary>

You can reach the profile page by navigating to: http://localhost:50010/profile

The profile of the user contains some different fields:

- FullName
- Email
- DeliveryAddress
- DeliveryAddressPostalCode
- DeliveryAddressPostalOffice
- DeliveryAddressCountry
- InvoiceAddress
- InvoiceAddressPostalCode
- InvoiceAddressPostalOffice
- InvoiceAddressCountry

The user has the option to:
- Use dummy data, then save
- Manually enter data, then save

![image](/images/profile_1.png)
</details>

<details>
    <summary>Products</summary>

You can reach the products page by navigating to: http://localhost:50010/

The products page consits of some main elements:
- **Filter line**, Ability to filter between products, names and prices
- **Grouping**: Displays the products in the group they belong to
- **Adding to cart**: By clicking the '+' icon you can add a product to the shopping cart 

![image](/images/products_1.png)

Explanation of the points found in the image above:
- 1) Navigates to the product catalog
- 2) Generic search function
- 3) Filter by categories
- 4) Filter from price
- 5) Filter to price
- 6) Add to shopping cart 

</details>

<details>
    <summary>Basket</summary>

You can reach the basket page by navigating to http://localhost:50010/basket or adding a product to the basket.

To add a product to the basket go to the product catalog and click the '+' icon:


![image](/images/basket_1.png)

When clicking the '+' icon you will be redirected to the basket like so:

![image](/images/basket_2.png)

Explanation of what you can do next, referings to the points in the image
- 1) Increase quantity of the item
- 2) Decrease quantity of the item
- 3) Continue shopping/go to product catalog
- 4) Start the order process

</details>

<details>
    <summary>Ordering</summary>


To continue with this step, check the manual for the basket above.

![image](/images/ordering_1.png)

When your basket looks ready for ordering proceed with clicking "Start Order".

![image](/images/ordering_2.png)

As you see in the image above a message will displayed telling you "Order started". From here there are 3 proceeding steps:

- Fill in delivery address (delivery address from profile will be automatically suggested)
- Fill in invoice address (invoice address from profile will be automatically suggested)
- Pay for order (The purchase service does not do anything with the data, but just returns 200 - Ok)

When these three steps are completed you can continue shopping.

Skipping the steps to fill out the address, you will get to the last step, payment.

![image](/images/ordering_3.png)

Payment will always execute as OK. Do not enter any personal information here.

![image](/images/ordering_4.png)

When payment completed, you can either continue shopping or check your orders

</details>

<details>
    <summary>Order history</summary>

You can reach the orders page redirection from basket, clicking the list-icon at the top or navigating to http://localhost:50010/orders


![image](/images/orders_1.png)

In the following image you can see three points that does the following:
- 1) You can navigate to the orders by clicking this icon
- 2) You can see your active orders
- 3) You can see your historic orders

![image](/images/orders_2.png)

By clicking on an order you can see the order details

</details>

### Admin manual

### Choices of Technology

#### RabbitMQ with MassTransit

**RabbitMQ** is a widely-used message broker that facilitates asynchronous communication in a distributed system, which is essential in a microservice architecture for decoupling services. It supports various messaging patterns, including publish/subscribe, which are crucial for building scalable and resilient systems.

- Decoupling of Services: RabbitMQ allows services to communicate without being tightly bound to each other, enhancing service independence and resilience
- Reliability and Consistency: It ensures message delivery even in cases of temporary service unavailability, preserving the consistency of operations across microservices
- Scalability: RabbitMQ effectively handles high throughput and can be scaled as the system grows

**MassTransit** is a lightweight service bus for building distributed applications using .NET. It acts as an abstraction layer over RabbitMQ, simplifying the development and maintenance of message-driven architectures.

- Ease of Use: MassTransit simplifies the integration with RabbitMQ, providing a more intuitive API for .NET developers
- Advanced Features: It offers features like saga coordination, scheduling, and support for various patterns that are beneficial in complex microservice interactions
- Better Error Handling and Monitoring: MassTransit provides enhanced error handling and monitoring capabilities, crucial for maintaining system health

#### Prometheus and Grafana

**Prometheus** is a powerful monitoring tool that collects and stores metrics as time-series data, allowing you to monitor the health and performance of your microservices.

- Real-Time Monitoring: Prometheus's strong querying capabilities allow real-time insight into microservice performance
- Scalability and Reliability: It is designed for reliability and scalability, handling large volumes of data efficiently

**Grafana** is an open-source platform for monitoring and observability and integrates seamlessly with Prometheus to provide visualizations of the collected data.

- Data Visualization: Grafana allows you to create comprehensive dashboards that visualize metrics from Prometheus, making it easier to understand and respond to the data
- Alerting: Grafana's alerting features enable proactive responses to potential issues, ensuring quick resolution of problems

### Overview

The primary objective of this project is to develop an e-commerce website. In line with this goal, we've chosen a microservices-based architecture to ensure high performance and flexibility.
Our system comprises four main services: product, basket, order, and purchase, each tailored to specific use cases defined by their names.
From the company's perspective, this system empowers administrators to view and manage orders, baskets, and products effectively.
For the end-users, we offer a straightforward frontend, enabling them to browse products, add items to their cart, place orders, and check out. We've also incorporated features for viewing order history.

### Prioritized user stories

#### Browse Product Catalog

#### As a user, I want to easily explore the range of products offered by the company. To streamline this process, I'd appreciate efficient filters and search functionality, ensuring I can quickly find specific products and categories. In today's fast-paced world, efficiency is crucial; otherwise, I might switch to a different vendor.

#### Place an Order

As a user, I'd like the ability to add products to my shopping cart as I browse the catalog. However, I don't always wish to place the order immediately. Sometimes, I prefer to accumulate items and review the total cost. Therefore, I suggest implementing a shopping cart where I can see the total and check stock availability for the products I've added. For convenience, I'd also appreciate the option to save delivery and invoice addresses in my profile settings.

#### Suggestions

As a user, I expect personalized product suggestions based on my previous orders. For example, if I primarily purchase technology products from the company, I don't want to sift through unrelated items like clothing. These suggestions should be prominently displayed when I enter the website, given my previous purchase history.

#### Track Order Delivery

As a user, I want the ability to track the status of my orders once they are placed. I expect real-time updates on the delivery process, including estimated delivery times, tracking numbers, and delivery notifications. This feature will enhance the overall shopping experience and keep me informed.

#### Product Reviews and Ratings

As a user, I'd appreciate the ability to read and submit product reviews and ratings. This will help me make informed purchasing decisions and contribute to a sense of community within the e-commerce platform.

### Requirements

Based on the user stories outlined above, several key requirements emerge. The most critical aspects revolve around addressing user-related needs. To meet these requirements, we must gather and manage data effectively. As a starting point, we should focus on the administrative functions.

###

### For administrative functions, the order of importance is as follows:

- Maintaining products
- Managing orders
- Maintaining baskets (for customer support)

### For user-related functions, the order of importance is as follows:

- User login
- Product browsing
- Adding products to the basket
- Placing orders
- Viewing order history
- Profile settings
###

### Architecture

The microservices architecture requires seamless communication between services, facilitated through a central gateway. This approach supports client-side load balancing and the identification of dependencies between services. The primary services in this project include user-service, basket-service, order-service, product-service, and purchase-service, all of which interact with the gateway.

### Description of services

#### Product service

The product service will handle querying of products from the user's perspective. But there will also be different CRUD functionality for the administration to manage their products.

#### Order service

The order service will handle queries and commands from the user's perspective. This will be things like, starting an order, filling in its details, see orders to confirmation, overview of paid orders and more. Upon different interaction's this service will publish different events, such as: order-started, order-confirmed, order-updated and order-delivered.

#### Purchase service

The purchase service will have a "dummy" implementation. When the user proceeds to checkout this service will be called and just return "Ok, the purchase went through".

#### User service

The user service will contain different information about the users(customers). Such as delivery address, invoice address with more.

#### Basket service

The basket service will be an in-memory service. This means that when the service is restarted the data will be lost. The service will listen on different events published by the order service to know when to clear the basket.

### Frontend

From a technical view the application will be made in a web framework called Blazor. Blazor has multiple rendering options, but I will go for server-side rendering. Over to specification, there will be two perspectives in the frontend. One will be the customer and the other will be the administration.

The administration will be able to:

- Create and change orders on the customer's behalf
- Add products to the basket on the customer's behalf
- Add/Update the product catalog

The customers will be able to:

- Manage information such as delivery address and invoice address
- Browse the product catalog
- Add/Remove/Increase/Decrease products in basket
- Start the order from the basket
- Overview of orders in progress


### Metrics and data visualization

To get a better overview of customers visiting different pages of the frontend i will implement some metrics. There metrics
will be registered through Prometheus and be visualized through Grafana.

#### Final notes

The course is taken in Java, but to challenge myself I have chosen to go with Dotnet C#. This to get a deeper understanding of different technologies and forces me to get my hands dirty. The different services will be made in ASP.NET Core which is a Dotnet technology for creating web servers (either apps, Api's or other). The solution itself will be containerized and to make the development process easier the solution can be started with docker compose. So be able to differentiate the different users there will be some simple authentication by using Google and OpenIdConnect.
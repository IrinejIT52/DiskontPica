-- Clear all tables and reset auto-increment counters to ensure a fresh, repeatable seed
TRUNCATE "OrderItem", "Orders", "Customer", "Product", "Administrator", "Category", "Country" RESTART IDENTITY CASCADE;

/* COUNTRIES */
INSERT INTO "Country" ("name") 
VALUES 
('Serbia'), 
('Bosnia and Herzegovina'), 
('Croatia'), 
('Netherlands'),
('Canada'),
('USA'),
('Germany'),
('Ireland');

/* CATEGORIES */
-- First, insert the main parent categories so they get their auto-generated IDs
INSERT INTO "Category" ("name", "description")
VALUES
('Non-Alcoholic', '0% alcohol drinks'),
('Alcoholic', 'Drinks containing alcohol');

-- Next, insert the subcategories, using subqueries to lookup the parent IDs dynamically
INSERT INTO "Category" ("superCategoryId", "name", "description")
VALUES
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Non-Alcoholic'), 'Soft drinks', 'Non-fizzy soft beverages'),
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Non-Alcoholic'), 'Fizzy drinks', 'Sodas and carbonated drinks'),
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Alcoholic'), 'Whiskey', 'High-end whiskey and bourbon'),
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Alcoholic'), 'Beer', 'World famous beers'),
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Alcoholic'), 'Wine', 'Red, white, and sparkling wines'),
((SELECT "categoryId" FROM "Category" WHERE "name" = 'Non-Alcoholic'), 'Energy drinks', 'Energy and sports drinks');

/* ADMINISTRATORS */
INSERT INTO "Administrator" ("name", "email", "password", "salt") 
VALUES 
('irinejkuzman', 'irinejkuzman@gmail.com', 'hashed_pwd_admin_1', 'random_salt_admin_1'),
('admin2', 'admin2@discountdrinks.com', 'hashed_pwd_admin_2', 'random_salt_admin_2');

/* PRODUCTS */
INSERT INTO "Product" ("name", "description", "price", "stock", "countryId", "categoryId", "adminId")
VALUES
('Vivia Water', 'Regular spring water 0.5L', 1.00, 1200, 1, 3, 1),
('Prolom Water', 'High pH mineral water 1L', 1.50, 500, 2, 3, 1),
('Coca-Cola', 'Carbonated soft drink 0.5L', 1.20, 800, 3, 4, 1),
('Fanta', 'Orange carbonated drink 0.5L', 1.10, 800, 3, 4, 1),
('Sprite', 'Lemon-lime carbonated drink 0.5L', 1.10, 600, 3, 4, 1),
('Red Bull', 'Energy Drink 0.25L', 2.20, 450, 4, 8, 1),
('Monster Energy', 'Energy Drink original 0.5L', 2.00, 400, 6, 8, 1),
('Canadian Whiskey', 'Pure Canadian whiskey 0.7L', 20.99, 150, 5, 5, 1),
('Bourbon', 'Bourbon whiskey aged in barrels 0.7L', 25.99, 70, 6, 5, 1),
('Jameson', 'Triple distilled Irish whiskey 0.7L', 22.50, 180, 8, 5, 1),
('Jelen Beer', 'Serbian original pale lager 0.5L', 1.30, 1100, 1, 6, 1),
('Heineken Beer', 'Premium Dutch lager beer 0.5L', 1.50, 700, 4, 6, 1),
('Zajecarsko Beer', 'Traditional Serbian red lager 0.5L', 1.20, 950, 1, 6, 1),
('Paulaner Munich', 'Authentic German Weissbier 0.5L', 1.80, 400, 7, 6, 1),
('Vranac Pro Corde', 'Premium Montenegrin dry red wine 0.75L', 8.50, 300, 2, 7, 1),
('Chardonnay Premium', 'Dry white wine 0.75L', 7.99, 250, 3, 7, 1);

/* CUSTOMERS */
INSERT INTO "Customer" ("name", "email", "address", "password", "salt")
VALUES 
('petar', 'petarz@gmail.com', 'Narodnog Fronta 20, Novi Sad', 'hashed_pwd_cust_1', 'random_salt_cust_1'),
('jovan', 'jovant@gmail.com', 'Futoski put 32, Novi Sad', 'hashed_pwd_cust_2', 'random_salt_cust_2'),
('milica', 'milicam@gmail.com', 'Bulevar Oslobodjenja 45, Novi Sad', 'hashed_pwd_cust_3', 'random_salt_cust_3');

/* ORDERS */
INSERT INTO "Orders" ("customerId", "finalPrice", "orderDate", "orderStatus", "orderType", "additionalInfo")
VALUES
(1, 5.00, '2024-03-11', 'CONFIRMED', 'REGULAR', 'No additional info'),
(2, 77.97, '2024-03-12', 'PENDING', 'ANNIVERSARY', 'Address: Ribarsko Ostrvo 14, Novi Sad'),
(1, 5.40, '2024-03-12', 'PENDING', 'REGULAR', 'No info'),
(3, 17.50, '2024-03-13', 'CONFIRMED', 'REGULAR', 'Leave at the reception');

/* ORDERITEMS */
INSERT INTO "OrderItem" ("orderId", "orderItemId", "productId", "quantity", "priceQuantity")
VALUES
(1, 1, 1, 5, 5.00),
(2, 1, 9, 3, 77.97),
(3, 1, 2, 2, 3.00),
(3, 2, 3, 2, 2.40),
(4, 1, 14, 5, 9.00),
(4, 2, 15, 1, 8.50);

--Loesung zu 1
select last_name from customer where last_name like 'a%' or last_name like 'b%' or last_name like 'c%'

--Loesung zu 2
select concat(first_name, ' ', last_name) from customer where active=1

--Loesung zu 3
select customer_id,email from customer ORDER BY length(email) DESC LIMIT 10

--Loesung zu 4
select count(customer_id) as `Anzahl Kunden`,store_id from customer group by store_id

--Loesung zu 5
select film_id,title,rental_rate from film where rating = 'G' and rental_rate<1.0


--Loesung zu 6
select COUNT(inventory_id) as 'Anzahl Kopien' , film_id   from inventory where store_id=1 group by film_id


--Loesung zu 7
select rating,store_id ,count(inventory_id) from film join inventory on film.film_id = inventory.film_id
group by store_id,rating
order by store_id , rating asc


--Loesung zu 8
select film.film_id,rating,title  from film
join film_category on film.film_id = film_category.film_id
join category on film_category.category_id = category.category_id
where category.name = 'Children' and film.rating in ('R','NC-17')


--Loesung zu 9
select customer_id , first_name, last_name, email, address.address, district, postal_code,city.city  from customer
join address on customer.address_id=address.address_id
join city on address.city_id = city.city_id
where city.country_id = (select country_id from country where country.country = 'Germany')


--Loesung zu 10
select count(customer_id),country.country  from customer
join address on customer.address_id=address.address_id
join city on address.city_id = city.city_id
join country on city.country_id = country.country_id
group by country.country
having count(customer_id)>=30


--Loesung zu 11
select first_name,last_name, monthname(payment_date),sum(amount) as 'Ausleihgebühren' from payment
join rental on payment.rental_id=rental.rental_id
join customer on rental.customer_id = customer.customer_id
where month(payment_date)<=6 and year(payment_date)=2005
group by first_name,last_name,monthname(payment_date)
order by month(payment_date) asc, Ausleihgebühren desc


--Loesung zu 12
select max(Summe) as 'Umsatz' ,Monat from(
select sum(amount) as 'Summe', monthname(rental_date) as 'Monat' from payment
join rental on payment.rental_id=rental.rental_id
) a



--Loesung zu 13
select name as 'Kategorie',store_id as 'Store', sum(amount) as 'Umsatz' from payment
join rental on payment.rental_id = rental.rental_id
join inventory on rental.inventory_id = inventory.inventory_id
join film on inventory.film_id = film.film_id
join film_category on film.film_id = film_category.film_id
join category on film_category.category_id = category.category_id
group by store_id,name


--Loesung zu 14
WITH RECURSIVE StaffAndSuperVisor AS
 ( SELECT staff_id,first_name,last_name,supervisor_id FROM staff
   WHERE staff_id=6
   UNION ALL
   SELECT f.staff_id,f.first_name,f.last_name,f.supervisor_id
   FROM staff AS f, StaffAndSuperVisor AS a
   WHERE f.staff_id = a.supervisor_id )
SELECT * FROM StaffAndSuperVisor;


--Loesung zu 15
SHOW COLUMNS FROM staff where columns.null = 'YES'

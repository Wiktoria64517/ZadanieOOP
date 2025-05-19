import random

class Car:
    def __init__(self, name):
        self.name = name
        self.lap_times = []

    def total_time(self):
        return sum(self.lap_times)

class Track:
    def __init__(self, name, laps):
        self.name = name
        self.laps = laps

class LapTimer:
    def __init__(self, car):
        self.car = car

    def record_lap(self):
        lap_time = random.randint(90, 120)
        self.car.lap_times.append(lap_time)
        return lap_time

class Race:
    def __init__(self, track, cars):
        self.track = track
        self.cars = cars

    def start(self):
        print(f"\n Start wyścigu na torze: {self.track.name} ({self.track.laps} okrążenia)\n")
        for car in self.cars:
            timer = LapTimer(car)
            print(f"Kierowca: {car.name}")
        for lap in range(1, self.track.laps + 1):
            time = timer.record_lap()
            print(f" Okrążenie {lap}: {time} sek")
            print(f" Łączny czas: {car.total_time()} sel\n")

    def show_podium(self):
        sorted_cars = sorted(self.cars, key=lambda c: c.total_time())
        print("Podium:\n")
        for i, car in enumerate(sorted_cars[:3], start=1):
            print(f"{i}. {car.name} - {car.total_time()} sek")

if __name__ == "__main__":
    track = Track("Prosty Tor", 3)

    cars = [
        Car("Lewis Hamilton"),
        Car("Max Verstappen"),
        Car("Charles Leclerc"),
        Car("Lando Norris")
    ]

    race = Race(track, cars)
    race.start()
    race.show_podium()
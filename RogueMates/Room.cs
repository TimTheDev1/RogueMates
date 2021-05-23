using System;
using System.Collections.Generic;
using System.Text;

namespace RogueMates
{
    class Room
    {
        public bool NeighboursGenerated { get; set; } = false;
        public bool hasEvent = false;
        public bool northEvent = false;
        public string eventSymbol = " ";
        public int roomCount = 0;

        private Room _north;
        public Room North
        {
            get
            {
                if (this._north != null && !this._north.NeighboursGenerated)
                    this._north.GenerateNeighbours();

                return this._north;
            }
            set
            {
                this._north = value;

                var random = new Random();

                if(random.Next(0,2) == 1)
                {
                    northEvent = true;
                    hasEvent = true;
                }
            }
        }

        public bool southEvent = false;

        private Room _south;
        public Room South
        {
            get
            {
                if (this._south != null && !this._south.NeighboursGenerated)
                    this._south.GenerateNeighbours();

                return this._south;
            }
            set
            {
                this._south = value;

                var random = new Random();

                if (random.Next(0, 2) == 1)
                {
                    southEvent = true;
                    hasEvent = true;
                }
            }
        }

        public bool eastEvent = false;

        private Room _east;
        public Room East
        {
            get
            {
                if (this._east != null && !this._east.NeighboursGenerated)
                    this._east.GenerateNeighbours();

                return this._east;
            }
            set
            {
                this._east = value;

                var random = new Random();

                if (random.Next(0, 2) == 1)
                {
                    eastEvent = true;
                    hasEvent = true;
                }
            }
        }

        public bool westEvent = false;

        private Room _west;
        public Room West
        {
            get
            {
                if (this._west != null && !this._west.NeighboursGenerated)
                    this._west.GenerateNeighbours();

                return this._west;
            }
            set
            {
                this._west = value;

                var random = new Random();

                if (random.Next(0, 2) == 1)
                {
                    westEvent = true;
                    hasEvent = true;
                }
            }
        }

        public Room(Direction cameFrom, Room room)
        {
            switch (cameFrom)
            {
                case Direction.NORTH: this.North = room; break;
                case Direction.SOUTH: this.South = room; break;
                case Direction.EAST: this.East = room; break;
                case Direction.WEST: this.West = room; break;
            }
        }

        public Room()
        {
            while (roomCount < 2)
            {
                this.GenerateNeighbours();

                Room[] rooms = { this.South, this.North, this.West, this.East };

                roomCount = 0;

                foreach (Room room in rooms)
                {
                    if (room != null)
                        roomCount += 1;
                }
            }
        }

        public void GenerateNeighbours()
        {
            Room[] rooms = { this.South, this.North, this.West, this.East};

            for(int room = 0; room < 4; room++)
            {
                var random = new Random();

                if (random.Next(0,2) == 1 && rooms[room] == null)
                {
                    rooms[room] = new Room((Direction)room, this);
                }
            }

            this.South = rooms[0];
            this.North = rooms[1];
            this.West = rooms[2];
            this.East = rooms[3];

            this.NeighboursGenerated = true;
        }
    }
}


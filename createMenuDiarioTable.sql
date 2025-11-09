USE RestauranteDB;

CREATE TABLE MenuDiario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PlatoPrincipalId INT NOT NULL,
    BebidaId INT NOT NULL,
    PostreId INT NOT NULL,
    Fecha DATE NOT NULL,
    PrecioTotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (PlatoPrincipalId) REFERENCES PlatoPrincipal(Id),
    FOREIGN KEY (BebidaId) REFERENCES Bebida(Id),
    FOREIGN KEY (PostreId) REFERENCES Postre(Id),
    CONSTRAINT UC_MenuDiario_Fecha UNIQUE (Fecha)
);

-- Índice para búsquedas por fecha
CREATE INDEX IX_MenuDiario_Fecha ON MenuDiario(Fecha);
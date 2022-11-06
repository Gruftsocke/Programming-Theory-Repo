/*********************************************************
 * Filename: UnitType.cs
 * Project : SchnabelSoftware.MyGame
 * Date    : 05.11.2022
 *
 * Author  : Daniel Schnabel
 * E-Mail  : info@schnabel-software.de
 *
 * Purpose : Programming-Theory-Repo
 *		     It shows the theory of the four pillars of OOP (object-oriented programming):
 *		     1. Abstraction
 *		     2. Inheritance
 *		     3. Polymorphism
 *		     4. Encapsulation.
 *
 * © Copyright by Schnabel-Software 2009-2022
 */
using System;

namespace SchnabelSoftware.MyGame.Units
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum UnitMask
    {
        None = 0,
        Worker = 1,
        Forklift = 2
    }

    public enum UnitType
    {
        Worker,
        Forklift
    }
}

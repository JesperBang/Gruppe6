using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodicSystem.ViewModel;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class TestCommands
    {
        [TestMethod]
        public void TestAddAtomCommand()
        {
            ViewModel vm = new ViewModel();

            //execute
            vm.AddAtomCommand.Execute(6);

            //verify
            //atom added
            Assert.AreEqual(1, vm.Atoms.Count);

            //undo
            vm.UndoCommand.Execute(null);

            //verify undo
            Assert.AreEqual(0, vm.Atoms.Count);

            //redo
            vm.RedoCommand.Execute(null);

            //verify redo
            Assert.AreEqual(1, vm.Atoms.Count);
        }

        [TestMethod]
        public void TestRemoveSelectionCommand()
        {
            ViewModel vm = new ViewModel();

            //prepare data
            Atom atom1 = new Atom(1);
            Atom atom2 = new Atom(1);
            Atom atom3 = new Atom(1);

            Binding binding1 = new Binding(atom1, atom2);
            Binding binding2 = new Binding(atom2, atom3);

            vm.Atoms.Add(atom1);
            vm.Atoms.Add(atom2);
            vm.Atoms.Add(atom3);

            vm.Bindings.Add(binding1);
            vm.Bindings.Add(binding2);

            //execute
            vm.SelectAllCommand.Execute(null);
            vm.RemoveModelCommand.Execute(null);

            //verify
            //selection removed
            Assert.AreEqual(0, vm.Atoms.Count);
            Assert.AreEqual(0, vm.Bindings.Count);

            //undo
            vm.UndoCommand.Execute(null);

            //verify undo
            Assert.AreEqual(3, vm.Atoms.Count);
            Assert.AreEqual(2, vm.Bindings.Count);

            //redo
            vm.RedoCommand.Execute(null);

            //verify redo
            Assert.AreEqual(0, vm.Atoms.Count);
            Assert.AreEqual(0, vm.Bindings.Count);
        } 

        [TestMethod]
        public void TestChangeBindingCommand()
        {
            ViewModel vm = new ViewModel();

            //prepare data
            Binding binding1 = new Binding();
            Binding binding2 = new Binding();
            Binding binding3 = new Binding();

            binding1.BindingState = Binding.TypeOfBinding.Single;
            binding2.BindingState = Binding.TypeOfBinding.Double;
            binding3.BindingState = Binding.TypeOfBinding.Triple;

            vm.Bindings.Add(binding1);
            vm.Bindings.Add(binding2);
            vm.Bindings.Add(binding3);

            // execute
            vm.ChangeBindingCommand.Execute(binding1.Id);
            vm.ChangeBindingCommand.Execute(binding2.Id);
            vm.ChangeBindingCommand.Execute(binding3.Id);

            //verify
            Assert.AreEqual(binding1.BindingState, Binding.TypeOfBinding.Double);
            Assert.AreEqual(binding2.BindingState, Binding.TypeOfBinding.Triple);
            Assert.AreEqual(binding3.BindingState, Binding.TypeOfBinding.Single);

            //undo
            vm.UndoCommand.Execute(null);
            vm.UndoCommand.Execute(null);
            vm.UndoCommand.Execute(null);

            //verify undo
            Assert.AreEqual(binding1.BindingState, Binding.TypeOfBinding.Single);
            Assert.AreEqual(binding2.BindingState, Binding.TypeOfBinding.Double);
            Assert.AreEqual(binding3.BindingState, Binding.TypeOfBinding.Triple);

            //redo
            vm.RedoCommand.Execute(null);
            vm.RedoCommand.Execute(null);
            vm.RedoCommand.Execute(null);

            //verify redo
            Assert.AreEqual(binding1.BindingState, Binding.TypeOfBinding.Double);
            Assert.AreEqual(binding2.BindingState, Binding.TypeOfBinding.Triple);
            Assert.AreEqual(binding3.BindingState, Binding.TypeOfBinding.Single);
        }
    }
}

namespace DataDictionary.Main.Enumerations
{
    // Handles the Scope Images. The Enum is the ScopeType enum.
    // This is the base set of images that are combined with Command or Status
    // overlays to create the images for the application.
    //
    // Functionally, the Resource could be used directly.
    // I found myself digging thru code to find references
    // and trying to be consistent. Using the Enum instead gave
    // me nice context specific code and consistent behavior.
    // It also abstracts the image away from the code making it
    // such that I only need to change it in one place instead
    // of doing difficult searches.

    static partial class ScopeIcon { } 
}

export enum VoteType {
    Standard,
    SmallGroup,
    LargeGroup
}

export class Post {
    id: number 
    creationDate: Date 
    text: string 
    author: string 
    votes: number
    smallGroupVotes: number
    largeGroupVotes: number
    published: boolean
    publishDate?: Date
    hidden: boolean 
}